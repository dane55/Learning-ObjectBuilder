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
using System.Text;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// �¼����ݷ����¼�<see cref="ILifetimeContainer"/>.
    /// </summary>
    public class LifetimeEventArgs : EventArgs
    {
        private object item;

        /// <summary>
        ///  ����������ʵ��
        /// </summary>
        public object Item
        {
            get { return item; }
        }

        /// <summary>
        /// ʵ���� <see cref="LifetimeEventArgs"/> ��
        /// </summary>
        /// <param name="item">����������ʵ��</param>
        public LifetimeEventArgs(object item)
        {
            this.item = item;
        }
    }
}
