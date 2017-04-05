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
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ�� <see cref="IPropertySetterInfo"/>��������������Ϣ
    /// </summary>
    public class PropertySetterInfo : IPropertySetterInfo
    {
        string name = null;
        PropertyInfo prop = null;
        IParameter value = null;

        /// <summary>
        /// ʵ���� <see cref="PropertySetterInfo"/>�࣬ �����������ƺ����õ�ֵ
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        public PropertySetterInfo(string name, IParameter value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// ʵ���� <see cref="PropertySetterInfo"/>�࣬ͨ��<see cref="PropertyInfo"/>����
        /// </summary>
        /// <param name="propInfo">PropertyInfo��Ϣ</param>
        /// <param name="value">������Ϣ</param>
        public PropertySetterInfo(PropertyInfo propInfo, IParameter value)
        {
            this.prop = propInfo;
            this.value = value;
        }

        /// <summary>
        /// �� <see cref="IPropertySetterInfo.SelectProperty"/> �в鿴����
        /// </summary>
        public PropertyInfo SelectProperty(IBuilderContext context, Type type, string id)
        {
            if (prop != null)
                return prop;

            return type.GetProperty(name);
        }

        /// <summary>
        /// �� <see cref="IPropertySetterInfo.GetValue"/> �в鿴����
        /// </summary>
        public object GetValue(IBuilderContext context, Type type, string id, PropertyInfo propInfo)
        {
            return value.GetValue(context);
        }
    }
}
