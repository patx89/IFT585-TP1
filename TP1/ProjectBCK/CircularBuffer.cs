using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TP1
{
    public class CircularBuffer<T> : INotifyPropertyChanged
    {

        public class CircularBufferEventArgs : EventArgs
        {
            private object _element;

            public object Element
            {
                get { return _element; }
            }

            public CircularBufferEventArgs(object element)
            {
                _element = element;
            }
        }

        #region Attributes

        private int _readPos = 0;
        private int _writePos = 0;
        private int _count = 0;

        private bool _isFull = false;
        private bool _isEmpty = true;
        
        private T[] values;

        #endregion

        #region Properties

        public bool IsFull
        {
            get { return this._isFull; }
            private set
            {
                if (this._isFull != value)
                {
                    this._isFull = value;
                    OnPropertyChanged("IsFull");
                    if (!value && FreedSpot != null) FreedSpot(this, new EventArgs());
                }
            }
        }

        public bool IsEmpty
        {
            get { return this._isEmpty; }
            private set
            {
                if (this._isEmpty != value)
                    this._isEmpty = value;
                OnPropertyChanged("IsEmpty");
            }
        }

        public int Count
        {
            get { return this._count; }
            private set
            {
                if (this._count != value)
                    this._count = value;
                OnPropertyChanged("Count");
            }
        }

        /// <summary>
        /// Is the length of the buffer. Is set a the creation and never changed after.
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        public T this[int index]
        {
            get
            {
                T item = default(T);

                if (index > -1 && index < Length)
                {
                    item = values[index];
                }

                return item;
            }
        }

        #endregion

        #region Constructors

        public CircularBuffer(int length)
        {
            Length = length;
            values = new T[Length];
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Triggers when the buffer is no longer full.
        /// </summary>
        public event EventHandler FreedSpot;

        /// <summary>
        /// Triggers when an element is added.
        /// </summary>
        public event EventHandler<CircularBufferEventArgs> ElementAdded;
        
        /// <summary>
        /// Triggers when an element is removed.
        /// </summary>
        public event EventHandler<CircularBufferEventArgs> ElementRemoved;


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #endregion
        

        #region Methods

        /// <summary>
        /// Determine if the value is  the default for the type T. Which means null for certain type or the default value of a non-nullable type.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDefaultValue(T val)
        {
            return EqualityComparer<T>.Default.Equals(val, default(T));
        }

        public T GetElement(int index)
        {
            return this[index];
        }        

        /// <summary>
        /// Iterate through the buffer to get the next non-null value and returns its position. Note: Returns -1 if none are found.
        /// </summary>
        /// <returns>The index of the next non-null value.</returns>
        public int GetNextElementIndex()
        {   
            int startingPos = _readPos;
            int elementPos = -1;
                        
            if (!IsEmpty)
            {
                do
                {
                    if (values[_readPos] != null)
                    {
                        elementPos = _readPos;
                        _readPos = (_readPos + 1) % Length;
                        break;
                    }
                    _readPos = (_readPos + 1) % Length;

                } while (_readPos != startingPos);
            }

            return elementPos;
        }

        /// <summary>
        /// Insert an element to the next empty spot. If none are found, it will return false;
        /// </summary>
        /// <param name="element">The element that will be added to the collection.</param>
        /// <returns>Whether or not the element was successfully added.</returns>
        public bool Push(T element)
        {
            if (IsFull) return false;

            int startingPos = _writePos;

            do{
                if (IsDefaultValue(values[_writePos]))
                {
                    values[_writePos] = element;
                    Count++;
                    if (ElementAdded != null) ElementAdded(this, new CircularBufferEventArgs(element));
                    if (Count == Length) IsFull = true;
                    return true;
                }
                _writePos = (_writePos + 1) % Length;

            } while (_writePos != startingPos);

            return false;
        }

        public T RemoveAt(int index)
        {
            T elementToRemove = default(T);

            if (values[index] != null)
            {
                elementToRemove = values[index];
                values[index] = default(T);

                Count--;
                if (ElementRemoved != null) ElementRemoved(this, new CircularBufferEventArgs(elementToRemove));
                if (Count == 0) IsEmpty = true;
                IsFull = false;
            }

            return elementToRemove;
        }


        #endregion


    }
}
