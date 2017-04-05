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
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ�� <see cref="IBuilderStrategy"/> ���ڴ�����ʵ����һ�����Ծ��������涨��ְ�ܲ��֣������Ĳ�ͬ��ְ�ܲ�������ͬ�����飬������������Ƕ�������
    /// </summary>
    public class CreationStrategy : BuilderStrategy
    {
        /// <summary>
        /// ��д <see cref="IBuilderStrategy.BuildUp"/>. ���ڹ����������������е���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="typeToBuild">��Ҫ�����Ķ��������</param>
        /// <param name="existing">һ��Ĭ�ϴ�null���󴴽��������������д���һ���µĶ���ʵ���������Ϊnull�����������������ж���</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns>�����Ķ���</returns>
        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            if (existing != null)  //�Ѵ��ڶ���
            {
                BuildUpExistingObject(context, typeToBuild, existing, idToBuild);
            }
            else
            {
                existing = BuildUpNewObject(context, typeToBuild, existing, idToBuild); //existing �����Ķ���ʵ����������ܶ���Χ�Ƹ�����ֵ��
            }
            //ִ����һ������
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }

        private void BuildUpExistingObject(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            RegisterObject(context, typeToBuild, existing, idToBuild);
        }

        /// <summary>
        /// �����¶���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="typeToBuild">��Ҫ�����Ķ��������</param>
        /// <param name="existing">һ��Ĭ�ϴ�null���󴴽��������������д���һ���µĶ���ʵ���������Ϊnull�����������������ж���</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        private object BuildUpNewObject(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            //CreationStrategy��������ʱ�����Policies��ȥ����Ӧ��ICreationPolicy��������ICreationPolicy����ȡ��
            //��ȡICreationPolicy����ConstuctorReflectionStrategy������ˡ�
            ICreationPolicy policy = context.Policies.Get<ICreationPolicy>(typeToBuild, idToBuild); //ConstructorReflectionStrategy

            if (policy == null)
            {
                if (idToBuild == null)
                    throw new ArgumentException(String.Format(CultureInfo.CurrentCulture,
                        Properties.Resources.MissingPolicyUnnamed, typeToBuild));
                else
                    throw new ArgumentException(String.Format(CultureInfo.CurrentCulture,
                        Properties.Resources.MissingPolicyNamed, typeToBuild, idToBuild));
            }

            try
            {
                //����ָ���������͵���ʵ����ϵͳAPI�����Ի�ȡһ��û�й��캯���Ķ���ʵ����
                existing = FormatterServices.GetSafeUninitializedObject(typeToBuild);
            }
            catch (MemberAccessException exception)
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, Properties.Resources.CannotCreateInstanceOfType, typeToBuild), exception);
            }
            //��Locatorע��ʵ��������ǵ���ģʽ������LifetimeContainer��ע��
            RegisterObject(context, typeToBuild, existing, idToBuild);
            //���ú��ʹ��������г�ʼ������
            InitializeObject(context, existing, idToBuild, policy);
            return existing;
        }


        /// <summary>
        /// ע����󣬵����Լ������������ڵĶ���
        /// </summary>
        /// <param name="context"></param>
        /// <param name="typeToBuild"></param>
        /// <param name="existing"></param>
        /// <param name="idToBuild"></param>
        private void RegisterObject(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            if (context.Locator != null)
            {
                //��ȡ��������ʵ��
                ILifetimeContainer lifetime = context.Locator.Get<ILifetimeContainer>(typeof(ILifetimeContainer), SearchMode.Local);
                //���������������
                if (lifetime != null)
                {
                    //��ȡ��������
                    ISingletonPolicy singletonPolicy = context.Policies.Get<ISingletonPolicy>(typeToBuild, idToBuild);
                    //��������������ǵ���
                    if (singletonPolicy != null && singletonPolicy.IsSingleton)
                    {
                        //��λ��(�洢���д洢����ʵ��)
                        context.Locator.Add(new DependencyResolutionLocatorKey(typeToBuild, idToBuild), existing);
                        lifetime.Add(existing);

                        if (TraceEnabled(context))
                            TraceBuildUp(context, typeToBuild, idToBuild, Properties.Resources.SingletonRegistered);
                    }
                }
            }
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="existing">һ��Ĭ�ϴ�null���󴴽��������������д���һ���µĶ���ʵ���������Ϊnull�����������������ж���</param>
        /// <param name="id">��Ҫ�����Ķ����Ψһ���</param>
        /// <param name="policy">�������캯�������Ĳ���</param>
        private void InitializeObject(IBuilderContext context, object existing, string id, ICreationPolicy policy)
        {
            //��Ҫ�����Ķ����Ĭ�Ϲ��캯��ʵ��
            Type type = existing.GetType();
            //��ȡ��ǰ����ʵ���Ĺ��캯��
            ConstructorInfo constructor = policy.SelectConstructor(context, type, id);

            if (constructor == null)
            {
                if (type.IsValueType)
                    return;
                throw new ArgumentException(Properties.Resources.NoAppropriateConstructor);
            }
            //��ȡ��ǰ���캯���еĲ���
            object[] parms = policy.GetParameters(context, type, id, constructor);
            //
            MethodBase method = (MethodBase)constructor;
            Guard.ValidateMethodParameters(method, parms, existing.GetType());
            if (TraceEnabled(context))
            {
                TraceBuildUp(context, type, id, Properties.Resources.CallingConstructor, ParametersToTypeList(parms));
            }
            //ʹ��ָ���Ĳ������õ�ǰʵ������ʾ�ķ������캯����
            method.Invoke(existing, parms);
        }

        private void ValidateCtorParameters(MethodBase methodInfo, object[] parameters, Type typeBeingBuilt)
        {
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                Guard.TypeIsAssignableFromType(paramInfos[i].ParameterType, parameters[i].GetType(), typeBeingBuilt);
            }
        }
    }
}