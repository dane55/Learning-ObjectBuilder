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
using System.Globalization;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ�� <see cref="IBuilderStrategy"/> ���������ò���
    /// </summary>
    /// <remarks>
    public class PropertySetterStrategy : BuilderStrategy
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
            if (existing != null)
                InjectProperties(context, existing, idToBuild);

            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }

        private void InjectProperties(IBuilderContext context, object obj, string id)
        {
            if (obj == null)
                return;

            Type type = obj.GetType();
            //��ȡ��������
            IPropertySetterPolicy policy = context.Policies.Get<IPropertySetterPolicy>(type, id);
            //����Ҫ�������
            if (policy == null)
                return;
            //Ϊÿһ�����Խ������
            foreach (IPropertySetterInfo propSetterInfo in policy.Properties.Values)
            {
                //��ȡ������Ϣ
                PropertyInfo propInfo = propSetterInfo.SelectProperty(context, type, id);

                if (propInfo != null)
                {
                    if (propInfo.CanWrite)
                    {
                        object value = propSetterInfo.GetValue(context, type, id, propInfo);

                        if (value != null)
                            Guard.TypeIsAssignableFromType(propInfo.PropertyType, value.GetType(), obj.GetType());

                        if (TraceEnabled(context))
                            TraceBuildUp(context, type, id, Properties.Resources.CallingProperty, propInfo.Name, propInfo.PropertyType.Name);
                        //��������ֵ��
                        propInfo.SetValue(obj, value, null);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format(
                            CultureInfo.CurrentCulture,
                            Properties.Resources.CannotInjectReadOnlyProperty,
                            type, propInfo.Name));
                    }
                }
            }
        }
    }
}