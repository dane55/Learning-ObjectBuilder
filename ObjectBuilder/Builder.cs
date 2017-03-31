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

using System.Diagnostics;
namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ������ <see cref="IBuilder{TStageEnum}"/> ʹ�� <see cref="BuilderStage"/>
    /// ��Ϊ��������Ĳ��ԣ����а���������Ĭ�ϵ�ObjectBuidler����
    /// </summary>
    public class Builder : BuilderBase<BuilderStage>
    {
        /// <summary>
        /// ʵ����һ�� <see cref="Builder"/> ��.
        /// </summary>
        public Builder()
            : this(null)
        {
        }

        /// <summary>
        /// ͨ��<see cref="IBuilderConfigurator{BuilderStage}"/>����ʵ����һ�� <see cref="Builder"/> ��.
        /// </summary>
        /// <param name="configurator">���������ö���ӿ�</param>
        public Builder(IBuilderConfigurator<BuilderStage> configurator)
        {
            Strategies.AddNew<TypeMappingStrategy>(BuilderStage.PreCreation);
            Strategies.AddNew<SingletonStrategy>(BuilderStage.PreCreation);
            Strategies.AddNew<ConstructorReflectionStrategy>(BuilderStage.PreCreation);
            Strategies.AddNew<PropertyReflectionStrategy>(BuilderStage.PreCreation);
            Strategies.AddNew<MethodReflectionStrategy>(BuilderStage.PreCreation);
            Strategies.AddNew<CreationStrategy>(BuilderStage.Creation);
            Strategies.AddNew<PropertySetterStrategy>(BuilderStage.Initialization);
            Strategies.AddNew<MethodExecutionStrategy>(BuilderStage.Initialization);
            Strategies.AddNew<BuilderAwareStrategy>(BuilderStage.PostInitialization);

            Policies.SetDefault<ICreationPolicy>(new DefaultCreationPolicy());

            if (configurator != null)
                configurator.ApplyConfiguration(this);
        }
    }
}