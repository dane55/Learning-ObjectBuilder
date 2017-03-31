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

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ��ʾ���������������Ķ���
    /// </summary>
    /// <typeparam name="TStageEnum">���ö�ٵķ��ͱ�ʾ���ʹ�������</typeparam>
    public interface IBuilderConfigurator<TStageEnum>
    {
        /// <summary>
        /// ������Ӧ�õ�IBuilder����������
        /// </summary>
        /// <param name="builder">(������������Ӧ�õ�)Builder����������</param>
        void ApplyConfiguration(IBuilder<TStageEnum> builder);
    }
}