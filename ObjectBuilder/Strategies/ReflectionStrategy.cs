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
    /// ������ <see cref="BuilderStrategy"/> �࣬����ע�봦�����Ļ���ͨ�ò��ԣ�ʵʩ������Ե�һ����ҪĿ�ľ���֧�ֶ��󴴽�ʱ������ע��
    /// </summary>
    public abstract class ReflectionStrategy<TMemberInfo> : BuilderStrategy
    {
        /// <summary>
        /// ��ȡ��Ҫ����ĳ�Ա��Ϣ
        /// </summary>
        /// <param name="context">���󴴽�������</param>
        /// <param name="typeToBuild">�������Ķ�������</param>
        /// <param name="existing">�Ѵ��ڵĶ��������null������CreationStrategy������</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns></returns>
        protected abstract IEnumerable<IReflectionMemberInfo<TMemberInfo>> GetMembers(IBuilderContext context, Type typeToBuild, object existing, string idToBuild);

        /// <summary>
        /// ��ÿһ���ҵ��ĳ�Ա�������Ҫ����Ļ�����������Ӧ����Ϣ���������͡�����ֵ�ȣ��������浽�����ĵĶ�Ӧ�ķ����У��Ա��ڴ����׶εõ���ȷ�Ĵ�������<see cref="BuilderStrategy.BuildUp"/> �в鿴����
        /// </summary>
        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            foreach (IReflectionMemberInfo<TMemberInfo> member in GetMembers(context, typeToBuild, existing, idToBuild))
            {
                if (MemberRequiresProcessing(member))
                {
                    //�������г�Ա�ĳ�Ա��Ϣ����ȡ��Ӧ��IParameter����
                    IEnumerable<IParameter> parameters = GenerateIParametersFromParameterInfos(member.GetParameters());
                    //������Ĳ�������Ĳ����С�
                    AddParametersToPolicy(context, typeToBuild, idToBuild, member, parameters);
                }
            }
            //������һ�����ԣ�����һ�����Ի�ȡ����Ĳ���ִ�й�����
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }

        /// <summary>
        /// �����Ժ�ֵ���в��뵽��������������
        /// </summary>
        /// <param name="context">The build context.</param>
        /// <param name="typeToBuild">The type being built.</param>
        /// <param name="idToBuild">The ID being built.</param>
        /// <param name="member">The member that's being reflected over.</param>
        /// <param name="parameters">The parameters used to satisfy the member call.</param>
        protected abstract void AddParametersToPolicy(IBuilderContext context, Type typeToBuild, string idToBuild, IReflectionMemberInfo<TMemberInfo> member, IEnumerable<IParameter> parameters);

        //��ȡIParameter���ϡ���ȡÿһ�����������ԣ��������Ե�CreateParameter������ȡ����ֵ
        private IEnumerable<IParameter> GenerateIParametersFromParameterInfos(ParameterInfo[] parameterInfos)
        {
            List<IParameter> result = new List<IParameter>();

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                ParameterAttribute attribute = GetInjectionAttribute(parameterInfo);
                result.Add(attribute.CreateParameter(parameterInfo.ParameterType));
            }

            return result;
        }
        //��ȡ�������Եķ�������ȡע��������ԡ�Ĭ����DependencyAttribute
        private ParameterAttribute GetInjectionAttribute(ParameterInfo parameterInfo)
        {
            ParameterAttribute[] attributes = (ParameterAttribute[])parameterInfo.GetCustomAttributes(typeof(ParameterAttribute), true);

            switch (attributes.Length)
            {
                case 0:
                    return new DependencyAttribute();

                case 1:
                    return attributes[0];

                default:
                    throw new InvalidAttributeException();
            }
        }

        /// <summary>
        /// �ж�һ�������Ƿ���Ҫ�������ע��
        /// </summary>
        /// <param name="member">The member being reflected over.</param>
        /// <returns>Returns true if the member should get injection; false otherwise.</returns>
        protected abstract bool MemberRequiresProcessing(IReflectionMemberInfo<TMemberInfo> member);
    }
}
