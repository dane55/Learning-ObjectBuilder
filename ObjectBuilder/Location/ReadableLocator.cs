//===============================================================================
// Microsoft patterns & practices
// ObjectBuilder Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Collections;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ�� <see cref="IReadableLocator"/>�ӿڣ�
    /// </summary>
    public abstract class ReadableLocator : IReadableLocator
    {
        private IReadableLocator parentLocator;

        /// <summary>
        /// ���ض�λ���е�����
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// ����λ��
        /// </summary>
        public virtual IReadableLocator ParentLocator
        {
            get { return parentLocator; }
        }

        /// <summary>
        /// �����λ����ֻ���ģ�����true
        /// </summary>
        public abstract bool ReadOnly { get; }

        /// <summary>
        /// ȷ����λ���Ƿ�����������Ķ���
        /// </summary>
        public bool Contains(object key)
        {
            return Contains(key, SearchMode.Up);
        }

        /// <summary>
        /// ȷ����λ���Ƿ�����������Ķ���
        /// </summary>
        public abstract bool Contains(object key, SearchMode options);

        /// <summary>
        /// �Զ�������������������һ����ʱ����
        /// </summary>
        public IReadableLocator FindBy(Predicate<KeyValuePair<object, object>> predicate)
        {
            return FindBy(SearchMode.Up, predicate);
        }

        /// <summary>
        /// �Զ�������������������һ����ʱֻ����λ������
        /// </summary>
        public IReadableLocator FindBy(SearchMode options, Predicate<KeyValuePair<object, object>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            if (!Enum.IsDefined(typeof(SearchMode), options)) throw new ArgumentException(Properties.Resources.InvalidEnumerationValue, "options");

            Locator results = new Locator();
            IReadableLocator currentLocator = this;

            while (currentLocator != null)
            {
                FindInLocator(predicate, results, currentLocator);
                currentLocator = options == SearchMode.Local ? null : currentLocator.ParentLocator;
            }
            return new ReadOnlyLocator(results);
        }

        private void FindInLocator(Predicate<KeyValuePair<object, object>> predicate, Locator results, IReadableLocator currentLocator)
        {
            foreach (KeyValuePair<object, object> kvp in currentLocator)
            {
                if (!results.Contains(kvp.Key) && predicate(kvp))
                {
                    results.Add(kvp.Key, kvp.Value);
                }
            }
        }

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        public TItem Get<TItem>()
        {
            return (TItem)Get(typeof(TItem));
        }

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        public TItem Get<TItem>(object key)
        {
            return (TItem)Get(key);
        }

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        public TItem Get<TItem>(object key, SearchMode options)
        {
            return (TItem)Get(key, options);
        }

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        public object Get(object key)
        {
            return Get(key, SearchMode.Up);
        }

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        public abstract object Get(object key, SearchMode options);

        /// <summary>
        /// ����һ��ѭ�����ʼ��ϵ�ö������
        /// </summary>
        public abstract IEnumerator<KeyValuePair<object, object>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// ���ô˶�λ���ĸ���λ��
        /// </summary>
        /// <param name="parentLocator">ʵ�� <see cref="IReadableLocator"/>�ӿڵĶ���ʵ��.</param>
        protected void SetParentLocator(IReadableLocator parentLocator)
        {
            this.parentLocator = parentLocator;
        }
    }
}
