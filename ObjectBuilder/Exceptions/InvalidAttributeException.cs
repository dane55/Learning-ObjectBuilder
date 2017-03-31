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
using System.Runtime.Serialization;
using System.Reflection;
using System.Globalization;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʹ������ע�����Ե���Ч���
    /// </summary>
    [Serializable]
    public class InvalidAttributeException : Exception
    {
        /// <summary>
        /// ��ʼ���쳣<see cref="InvalidAttributeException"/>��
        /// </summary>
        public InvalidAttributeException()
        {
        }

        /// <summary>
        /// ��ʼ���쳣<see cref="InvalidAttributeException"/>��
        /// </summary>
        /// <param name="message">�����쳣ԭ��Ĵ�����Ϣ��</param>
        public InvalidAttributeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// ��ʼ���쳣<see cref="InvalidAttributeException"/>��
        /// </summary>
        /// <param name="message">�����쳣ԭ��Ĵ�����Ϣ��</param>
        /// <param name="exception">���µ�ǰ�쳣���쳣�����δָ���ڲ��쳣������һ�� null ���ã��� Visual Basic ��Ϊ Nothing����</param>
        public InvalidAttributeException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// ��ʼ���쳣<see cref="InvalidAttributeException"/>��
        /// </summary>
        /// <param name="type">����</param>
        /// <param name="memberName">��Ա����</param>
        public InvalidAttributeException(Type type, string memberName)
            : base(String.Format(CultureInfo.CurrentCulture, Properties.Resources.InvalidAttributeCombination, type, memberName))
        {
        }

        /// <summary>
        /// ��ʼ���쳣<see cref="InvalidAttributeException"/>��
        /// </summary>
        protected InvalidAttributeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
