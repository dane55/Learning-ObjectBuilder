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
    /// ������ <see cref="BuilderStrategy"/> ����ִ�в���
    /// </summary>
    public class MethodExecutionStrategy : BuilderStrategy
    {
        /// <summary>
        /// ���ڹ����������������е���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="typeToBuild">��Ҫ�����Ķ��������</param>
        /// <param name="existing">һ��Ĭ�ϴ�null���󴴽��������������д���һ���µĶ���ʵ���������Ϊnull�����������������ж���</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns>�����Ķ���</returns>
        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            //ִ������
            ApplyPolicy(context, existing, idToBuild);
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }

        /// <summary>
        /// ִ������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        private void ApplyPolicy(IBuilderContext context, object obj, string id)
        {
            if (obj == null)
                return;

            Type type = obj.GetType();
            //��ȡ����ִ������
            IMethodPolicy policy = context.Policies.Get<IMethodPolicy>(type, id);
            //ֱ�ӷ��ء�
            if (policy == null)
                return;
            //����ÿһ��������
            foreach (IMethodCallInfo methodCallInfo in policy.Methods.Values)
            {
                MethodInfo methodInfo = methodCallInfo.SelectMethod(context, type, id);

                if (methodInfo != null)
                {
                    object[] parameters = methodCallInfo.GetParameters(context, type, id, methodInfo);
                    Guard.ValidateMethodParameters(methodInfo, parameters, obj.GetType());
                    if (TraceEnabled(context))
                    {
                        TraceBuildUp(context, type, id, Properties.Resources.CallingMethod, methodInfo.Name, ParametersToTypeList(parameters));
                    }
                    //���÷�����
                    methodInfo.Invoke(obj, parameters);
                }
            }
        }
    }
}