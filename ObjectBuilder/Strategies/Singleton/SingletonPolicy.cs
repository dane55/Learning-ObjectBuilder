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
    /// ʵ�ֽӿ�<see cref="ISingletonPolicy"/>����
    /// </summary>
    public class SingletonPolicy : ISingletonPolicy
    {
        private bool isSingleton;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="isSingleton">�����Ƿ�Ϊ����</param>
        public SingletonPolicy(bool isSingleton)
        {
            this.isSingleton = isSingleton;
        }

        /// <summary>
        /// �� <see cref="ISingletonPolicy.IsSingleton"/> �в鿴����
        /// </summary>
        public bool IsSingleton
        {
            get { return isSingleton; }
        }
    }
}
