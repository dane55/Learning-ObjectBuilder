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
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ�� <see cref="ILifetimeContainer"/>.
    /// </summary>
    public class LifetimeContainer : ILifetimeContainer
    {
        private List<object> items = new List<object>();

        /// <summary>
        /// �����������������е�������
        /// </summary>
        public int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// ���һ������ʵ������������������
        /// </summary>
        /// <param name="item">Ҫ��ӵ���Ŀ</param>
        public void Add(object item)
        {
            items.Add(item);
        }

        /// <summary>
        /// �����������Ƿ�����������������
        /// </summary>
        /// <param name="item">Ҫ������Ŀ</param>
        /// <returns>�������������������������У��򷵻�true�����򷵻�false��</returns>
        public bool Contains(object item)
        {
            return items.Contains(item);
        }

        /// <summary>
        /// �����������Լ������а������κζ���
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// ���ٶ���
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                List<object> itemsCopy = new List<object>(items);
                itemsCopy.Reverse();

                foreach (object o in itemsCopy)
                {
                    IDisposable d = o as IDisposable;

                    if (d != null)
                        d.Dispose();
                }

                items.Clear();
            }
        }

        /// <summary>
        /// �����������������Ƴ���������
        /// </summary>
        /// <param name="item">Ҫɾ������Ŀ</param>
        public void Remove(object item)
        {
            if (!items.Contains(item))
                return;

            items.Remove(item);
        }

        /// <summary>
        /// ����ѭ������<see cref="List{T}"/> ��ö������
        /// </summary>
        /// <returns>���� <see cref="List{T}"/> �� <see cref="List{T}.Enumerator"/>��</returns>
        public IEnumerator<object> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
