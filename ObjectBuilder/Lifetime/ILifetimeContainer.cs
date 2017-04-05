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
    /// ������������
    /// </summary>
    /// <remarks>
    /// ���д洢��Locator�Ķ��󶼽��洢����������ã���Locator��ֱ����洢�Ķ�������ϵ��
    /// ��ˣ���Locator�洢�Ķ����һ��ʱ��ͻᱻGC�Զ��ռ���ֻ�е������洢����������������ʱ��
    /// �ö���Ų��ܱ�GC�ռ���ӦΪ�������ڴ洢���洢�˶����ʵ����
    /// </remarks>
    public interface ILifetimeContainer : IEnumerable<object>, IDisposable
    {
        /// <summary>
        /// �����������������е�������
        /// </summary>
        int Count { get; }

        /// <summary>
        /// ���һ������ʵ������������������
        /// </summary>
        /// <param name="item">Ҫ��ӵ���Ŀ</param>
        void Add(object item);

        /// <summary>
        /// �����������Ƿ�����������������
        /// </summary>
        /// <param name="item">Ҫ������Ŀ</param>
        /// <returns>�������������������������У��򷵻�true�����򷵻�false��</returns>
        bool Contains(object item);

        /// <summary>
        /// �����������������Ƴ���������
        /// </summary>
        /// <param name="item">Ҫɾ������Ŀ</param>
        void Remove(object item);
    }
}