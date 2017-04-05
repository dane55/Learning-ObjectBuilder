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
using System.Globalization;
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ������������ԣ��˲�����Ҫ������������һ����ѡ�����������Ǵ����ù��캯�������б�Ȼ������<see cref="ConstructorPolicy"/>���߷���(��������)
    /// </summary>
    public class ConstructorReflectionStrategy : ReflectionStrategy<ConstructorInfo>
    {
        /// <summary>
        /// ��ȡ��Ҫ����ĳ�Ա��Ϣ
        /// </summary>
        /// <param name="context">���󴴽�������</param>
        /// <param name="typeToBuild">�������Ķ�������</param>
        /// <param name="existing">�Ѵ��ڵĶ��������null������CreationStrategy������</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns></returns>
        protected override IEnumerable<IReflectionMemberInfo<ConstructorInfo>> GetMembers(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            //�½�һ�����캯������
            List<IReflectionMemberInfo<ConstructorInfo>> result = new List<IReflectionMemberInfo<ConstructorInfo>>();
            //��ȡ�������ԡ�
            ICreationPolicy existingPolicy = context.Policies.Get<ICreationPolicy>(typeToBuild, idToBuild);
            //���existingPolicy��DefaultCreationPolicy���ߣ�existingΪnull����existingPolicy��Ϊnull
            if (existing == null && (existingPolicy == null || existingPolicy is DefaultCreationPolicy))
            {
                ConstructorInfo injectionCtor = null; //������Ϊ��ǰĬ��ע��Ĺ��캯��
                ConstructorInfo[] ctors = typeToBuild.GetConstructors();  //�������Ķ���Ĺ��캯������
                //��ֻ����һ�����캯��ʱ��ָ����ǰ���캯��ΪĬ��ע�빹�캯��
                if (ctors.Length == 1)
                {
                    injectionCtor = ctors[0];
                }
                else
                {
                    //������������캯���ǣ�
                    foreach (ConstructorInfo ctor in ctors)
                    {
                        if (Attribute.IsDefined(ctor, typeof(InjectionConstructorAttribute)))  //ͨ��ѭ����⹹�캯���Ƿ�������InjectionConstructorAttribute����˼�ǵ�����������캯��ʱ��ֻ�б��InjectionConstructor���Բ�����Ч�Ĺ��캯��
                        {
                            //������Σ��׳��쳣����Ҫע������͵Ĺ��캯��ֻ�ܱ��һ��ΪInjectionConstructorAttribute
                            if (injectionCtor != null)
                            {
                                throw new InvalidAttributeException();
                            }
                            injectionCtor = ctor;
                        }
                    }
                }

                if (injectionCtor != null)
                {
                    result.Add(new ReflectionMemberInfo<ConstructorInfo>(injectionCtor));
                }

            }

            return result;
        }

        /// <summary>
        /// �� <see cref="ReflectionStrategy{T}.AddParametersToPolicy"/> �в鿴���࣬����ICreationPolicyΪConstructorPolicy��
        /// </summary>
        protected override void AddParametersToPolicy(IBuilderContext context, Type typeToBuild, string idToBuild, IReflectionMemberInfo<ConstructorInfo> member, IEnumerable<IParameter> parameters)
        {
            ConstructorPolicy policy = new ConstructorPolicy();

            foreach (IParameter parameter in parameters)
                policy.AddParameter(parameter);

            context.Policies.Set<ICreationPolicy>(policy, typeToBuild, idToBuild);
        }

        /// <summary>
        /// �� <see cref="ReflectionStrategy{T}.MemberRequiresProcessing"/> �в鿴���ֻ࣬��һ����ֱ�ӷ���true
        /// </summary>
        protected override bool MemberRequiresProcessing(IReflectionMemberInfo<ConstructorInfo> member)
        {
            return true;
        }
    }
}
