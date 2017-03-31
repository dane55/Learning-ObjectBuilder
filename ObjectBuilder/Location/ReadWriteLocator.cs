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

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ�� <see cref="IReadWriteLocator"/>�ӿڣ��ɶ�д�Ķ�λ��
    /// </summary>
    public abstract class ReadWriteLocator : ReadableLocator, IReadWriteLocator
    {
        /// <summary>
        /// �Ƿ�ֻ����Ĭ��Ϊfalse�����ɶ�
        /// </summary>
        public override bool ReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// ���һ�����󵽶�λ����������ļ�
        /// </summary>
        public abstract void Add(object key, object value);

        /// <summary>
        /// �Ӷ�λ���Ƴ�����
        /// </summary>
        public abstract bool Remove(object key);
    }
}
