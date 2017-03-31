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
    /// �޷�����������Ϊ����������ע�����Ͳ�����
    /// </summary>
    [Serializable]
    public class IncompatibleTypesException : Exception
    {
        /// <summary>
        /// ��ʼ���쳣<see cref="IncompatibleTypesException"/>��
        /// </summary>
        public IncompatibleTypesException()
        {
        }

        /// <summary>
        /// ��ʼ���쳣<see cref="IncompatibleTypesException"/>��
        /// </summary>
        /// <param name="message">�����쳣ԭ��Ĵ�����Ϣ��</param>
        public IncompatibleTypesException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// ��ʼ���쳣<see cref="IncompatibleTypesException"/>��
        /// </summary>
        /// <param name="message">�����쳣ԭ��Ĵ�����Ϣ��</param>
        /// <param name="exception">���µ�ǰ�쳣���쳣�����δָ���ڲ��쳣������һ�� null ���ã��� Visual Basic ��Ϊ Nothing����</param>
        public IncompatibleTypesException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// ��ʼ���쳣<see cref="IncompatibleTypesException"/>��
        /// </summary>
        protected IncompatibleTypesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
