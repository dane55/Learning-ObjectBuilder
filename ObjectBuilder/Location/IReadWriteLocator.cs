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

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// �ɶ�ȡ��д��Ķ�λ��
    /// </summary>
    public interface IReadWriteLocator : IReadableLocator
    {
        /// <summary>
        /// ������󵽶�λ��
        /// </summary>
        /// <param name="key">Key��Ψһ��ʶ</param>
        /// <param name="value">Ҫע��Ķ���</param>
        /// <exception cref="ArgumentNullException">Key��valueΪnull.</exception>
        void Add(object key, object value);

        /// <summary>
        /// �Ӷ�λ����ɾ��һ����������ɹ������棬���򷵻ؼ�
        /// </summary>
        /// <param name="key">Key��Ψһ��ʶ</param>
        /// <returns>����ڶ�λ�����ҵ��ö����򷵻�true�����򷵻�false</returns>
        /// <exception cref="ArgumentNullException">KeyΪnull.</exception>
        bool Remove(object key);
    }
}