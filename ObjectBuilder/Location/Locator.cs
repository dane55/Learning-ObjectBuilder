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

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ������ <see cref="ReadWriteLocator"/>�࣬����һ����λ���ɶ�д��ObjectBuilder�ڲ�Ĭ�ϵĶ�λ����һ��������ݵĶ�λ��ʵ���͵�����ʵ��
    /// </summary>
    public class Locator : ReadWriteLocator
    {
        private WeakRefDictionary<object, object> references = new WeakRefDictionary<object, object>();

        /// <summary>
        /// ���캯������������λ��
        /// </summary>
        public Locator()
            : this(null)
        {
        }

        /// <summary>
        /// ���캯���������Ӷ�λ��
        /// </summary>
        /// <param name="parentLocator">���ø���λ��</param>
        public Locator(IReadableLocator parentLocator)
        {
            SetParentLocator(parentLocator);
        }

        /// <summary>
        /// ���ض�λ���е�����
        /// </summary>
        public override int Count
        {
            get { return references.Count; }
        }

        /// <summary>
        /// ���һ�����󵽶�λ����������ļ�
        /// </summary>
        public override void Add(object key, object value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            references.Add(key, value);
        }

        /// <summary>
        /// ȷ����λ���Ƿ�����������Ķ���
        /// </summary>
        public override bool Contains(object key, SearchMode options)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (!Enum.IsDefined(typeof(SearchMode), options))
                throw new ArgumentException(Properties.Resources.InvalidEnumerationValue, "options");

            if (references.ContainsKey(key))
                return true;

            if (options == SearchMode.Up && ParentLocator != null)
                return ParentLocator.Contains(key, options);

            return false;
        }

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        public override object Get(object key, SearchMode options)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (!Enum.IsDefined(typeof(SearchMode), options))
                throw new ArgumentException(Properties.Resources.InvalidEnumerationValue, "options");

            if (references.ContainsKey(key))
                return references[key];

            if (options == SearchMode.Up && ParentLocator != null)
                return ParentLocator.Get(key, options);

            return null;
        }

        /// <summary>
        /// �Ӷ�λ���Ƴ�����
        /// </summary>
        public override bool Remove(object key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return references.Remove(key);
        }

        /// <summary>
        /// ����һ��ѭ�����ʼ��ϵ�ö������
        /// </summary>
        public override IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return references.GetEnumerator();
        }
    }
}
