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
    /// ������ <see cref="IReadableLocator"/>���ͣ�ֻ���Ķ�λ��.
    /// </summary>
    public class ReadOnlyLocator : ReadableLocator
    {
        private IReadableLocator innerLocator;

        /// <summary>
        /// ���캯��������һ��ֻ���Ķ�λ��
        /// </summary>
        /// <param name="innerLocator">Ĭ�϶�λ��</param>
        public ReadOnlyLocator(IReadableLocator innerLocator)
        {
            if (innerLocator == null)
                throw new ArgumentNullException("innerLocator");

            this.innerLocator = innerLocator;
        }

        /// <summary>
        /// ���ض�λ���е�����
        /// </summary>
        public override int Count
        {
            get { return innerLocator.Count; }
        }

        /// <summary>
        /// ����λ��
        /// </summary>
        public override IReadableLocator ParentLocator
        {
            get
            {
                return new ReadOnlyLocator(innerLocator.ParentLocator);
            }
        }

        /// <summary>
        /// �����λ����ֻ���ģ�����true
        /// </summary>
        public override bool ReadOnly
        {
            get { return true; }
        }

        /// <summary>
        ///  ȷ����λ���Ƿ�����������Ķ���
        /// </summary>
        public override bool Contains(object key, SearchMode options)
        {
            return innerLocator.Contains(key, options);
        }

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        public override object Get(object key, SearchMode options)
        {
            return innerLocator.Get(key, options);
        }

        /// <summary>
        /// ����һ��ѭ�����ʼ��ϵ�ö������
        /// </summary>
        public override IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return innerLocator.GetEnumerator();
        }
    }
}
