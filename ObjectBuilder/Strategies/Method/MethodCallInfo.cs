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
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ��ʾ��Ҫִ�з���ִ�в��Եķ�������Ϣ
    /// </summary>
    public class MethodCallInfo : IMethodCallInfo
    {
        private MethodInfo method;
        private string methodName;
        private List<IParameter> parameters;

        /// <summary>
        /// ʵ���� <see cref="MethodCallInfo"/>��
        /// </summary>
        /// <param name="methodName">��Ҫִ�еķ�������</param>
        public MethodCallInfo(string methodName)
            : this(methodName, (MethodInfo)null, null)
        {
        }

        /// <summary>
        /// ʵ���� <see cref="MethodCallInfo"/>��
        /// </summary>
        /// <param name="methodName">��Ҫִ�еķ�������</param>
        /// <param name="parameters">�����б�</param>
        public MethodCallInfo(string methodName, params object[] parameters)
            : this(methodName, null, ObjectsToIParameters(parameters))
        {
        }

        /// <summary>
        /// ʵ���� <see cref="MethodCallInfo"/>��
        /// </summary>
        /// <param name="methodName">��Ҫִ�еķ�������</param>
        /// <param name="parameters">�����б�</param>
        public MethodCallInfo(string methodName, params IParameter[] parameters)
            : this(methodName, null, parameters)
        {
        }

        /// <summary>
        /// ʵ���� <see cref="MethodCallInfo"/>��
        /// </summary>
        /// <param name="methodName">��Ҫִ�еķ�������</param>
        /// <param name="parameters">�����б�</param>
        public MethodCallInfo(string methodName, IEnumerable<IParameter> parameters)
            : this(methodName, null, parameters)
        {
        }

        /// <summary>
        /// ʵ���� <see cref="MethodCallInfo"/>��
        /// </summary>
        /// <param name="method">��Ҫִ�еķ�����Ϣ</param>
        public MethodCallInfo(MethodInfo method)
            : this(null, method, null)
        {
        }

        /// <summary>
        /// ʵ���� <see cref="MethodCallInfo"/>��
        /// </summary>
        /// <param name="method">��Ҫִ�еķ�����Ϣ</param>
        /// <param name="parameters">�����б�</param>
        public MethodCallInfo(MethodInfo method, params IParameter[] parameters)
            : this(null, method, parameters)
        {
        }

        /// <summary>
        /// ʵ���� <see cref="MethodCallInfo"/>��
        /// </summary>
        /// <param name="method">��Ҫִ�еķ�����Ϣ</param>
        /// <param name="parameters">�����б�</param>
        public MethodCallInfo(MethodInfo method, IEnumerable<IParameter> parameters)
            : this(null, method, parameters)
        {
        }

        //�������ƣ���Ϣ�Ͳ����б�
        private MethodCallInfo(string methodName, MethodInfo method, IEnumerable<IParameter> parameters)
        {
            this.methodName = methodName;
            this.method = method;
            this.parameters = new List<IParameter>();

            if (parameters != null)
                foreach (IParameter param in parameters)
                    this.parameters.Add(param);
        }

        /// <summary>
        /// ��ȡע��ķ�����ѡ�����������ϵĵķ���
        /// </summary>
        public MethodInfo SelectMethod(IBuilderContext context, Type type, string id)
        {
            if (method != null)
                return method;

            List<Type> types = new List<Type>();

            foreach (IParameter param in parameters)
                types.Add(param.GetParameterType(context));

            return type.GetMethod(methodName, types.ToArray());
        }

        /// <summary>
        /// ��ȡ���÷����Ĳ����б�
        /// </summary>
        public object[] GetParameters(IBuilderContext context, Type type, string id, MethodInfo method)
        {
            List<object> values = new List<object>();

            foreach (IParameter param in parameters)
                values.Add(param.GetValue(context));

            return values.ToArray();
        }

        private static IEnumerable<IParameter> ObjectsToIParameters(object[] parameters)
        {
            List<IParameter> results = new List<IParameter>();

            if (parameters != null)
                foreach (object parameter in parameters)
                    results.Add(new ValueParameter(parameter.GetType(), parameter));

            return results.ToArray();
        }
    }
}
