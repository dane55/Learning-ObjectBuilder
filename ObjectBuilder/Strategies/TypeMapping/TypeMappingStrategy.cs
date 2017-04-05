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
    /// ������ <see cref="BuilderStrategy"/>������ӳ����ԣ����ȹ������Ǵ洢������������ӳ����ԣ����ڽ����ȷ����������
    /// ���½�һ��<see cref="DependencyResolutionLocatorKey"/>����
    /// Ȼ���ȡ�ö����Ӧ��<see cref="ITypeMappingPolicy"/>���߷���(��������)��
    /// ���øö����Map��������ȡ��ȷ�����͡�ID��������������ȷ�����ʹ��ݸ���һ������(��������)��
    /// </summary>
    public class TypeMappingStrategy : BuilderStrategy
    {

        /// <summary>
        ///  ��д <see cref="BuilderStrategy.BuildUp"/>.
        /// </summary>
        /// <param name="context">��ǰ���Ե�ִ��������</param>
        /// <param name="t">���������������</param>
        /// <param name="existing">��ǰ��Ҫ�����Ķ����ʵ����һ�㴫null���Ѵ��ڵĶ���ʵ��</param>
        /// <param name="id">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns></returns>
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            //������t������Ҫ�������Ķ������ͣ�existing���Ѵ��ڵĶ���ʵ����id��Ҫ������������ַ�����ʶ
            //����ʹ�����ͺ��ַ�����ʶ����һ�� DependencyResolutionLocatorKey ����ͨ������Ϊ��λ���еĶ���ı�ʶ��
            DependencyResolutionLocatorKey result = new DependencyResolutionLocatorKey(t, id);
            //context.Policies�洢�˵�ǰ�ܵ������еĶ�������
            //��ȡ��������ITypeMappingPolicy
            ITypeMappingPolicy policy = context.Policies.Get<ITypeMappingPolicy>(t, id);           
            if (policy != null)
            {
                result = policy.Map(result); //ӳ����Ե������ǽ���ǰ��Ҫ��������������������Ե�ӳ���ϵ��һЩ΢����һ����ָ�ӿڣ�������֮��
                TraceBuildUp(context, t, id, Properties.Resources.TypeMapped, result.Type, result.ID ?? "(null)");
                //����result.Type�Ƿ�������t�����߱���result.Type��t����ͬ���ͣ���������������ӳ�����Ч�������׳��쳣
                Guard.TypeIsAssignableFromType(t, result.Type, t);
            }
            //����ȷ�����ʹ��ݸ���һ������ִ�й�����
            return base.BuildUp(context, result.Type, existing, result.ID);
        }
    }
}