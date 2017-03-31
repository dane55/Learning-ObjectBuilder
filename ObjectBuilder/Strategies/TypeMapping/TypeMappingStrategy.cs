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

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ������ <see cref="IBuilderStrategy"/> ����ӳ�����ͺ�ID
    /// </summary>
    public class TypeMappingStrategy : BuilderStrategy
    {

        /// <summary>
        ///  ��д <see cref="BuilderStrategy.BuildUp"/>.
        /// </summary>
        /// <param name="context">��ǰ���Ե�ִ��������</param>
        /// <param name="t">��Ҫ�����Ķ�������</param>
        /// <param name="existing">��ǰ��Ҫ�����Ķ����ʵ����һ�㴫null</param>
        /// <param name="id">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns></returns>
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            //��ǰ���һ������Ҫ��������
            DependencyResolutionLocatorKey result = new DependencyResolutionLocatorKey(t, id);
            //context.Policies�洢�˵�ǰ�ܵ������еĶ�������
            //��ȡ��������ITypeMappingPolicy
            ITypeMappingPolicy policy = context.Policies.Get<ITypeMappingPolicy>(t, id);

            if (policy != null)
            {
                result = policy.Map(result);
                TraceBuildUp(context, t, id, Properties.Resources.TypeMapped, result.Type, result.ID ?? "(null)");
                Guard.TypeIsAssignableFromType(t, result.Type, t);
            }

            return base.BuildUp(context, result.Type, existing, result.ID);
        }
    }
}