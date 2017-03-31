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
using System.Runtime.Serialization;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// �޷�����������Զ����쳣
    /// </summary>
    [Serializable]
    public class DependencyMissingException : Exception
    {
        /// <summary>
        /// ʵ���� <see cref="DependencyMissingException"/> ��
        /// </summary>
        public DependencyMissingException()
        {
        }

        /// <summary>
        /// ʵ���� <see cref="DependencyMissingException"/> ��
        /// </summary>
        /// <param name="message">�����쳣ԭ��Ĵ�����Ϣ��</param>
        public DependencyMissingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// ʵ���� <see cref="DependencyMissingException"/> ��
        /// </summary>
        /// <param name="message">�����쳣ԭ��Ĵ�����Ϣ��</param>
        /// <param name="exception">���µ�ǰ�쳣���쳣�����δָ���ڲ��쳣������һ�� null ���ã��� Visual Basic ��Ϊ Nothing����</param>
        public DependencyMissingException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// ʵ���� <see cref="DependencyMissingException"/> ��
        /// </summary>
        protected DependencyMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
