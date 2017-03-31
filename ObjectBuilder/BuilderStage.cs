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
	/// ObjectBuilder���󴴽�˳��
	/// </summary>
	public enum BuilderStage
	{
        /// <summary>
        /// ����׶εĲ����ڴ���֮ǰ���С��ڴ˽׶������ĵ��͹������ܰ���ʹ�÷��佫�������õ����������Ժ�ʹ�õ��������еĲ��ԡ�
        /// </summary>
        PreCreation,

        /// <summary>
        /// ������׶���ֻ��һ�����������Ĵ�������
        /// </summary>
        Creation,

        /// <summary>
        /// ������׶εĹ����Դ�������Ĳ��ԡ��ڴ˽׶������ĵ��͹������ܰ�������ע��ͷ������á�
        /// </summary>
        Initialization,

        /// <summary>
        /// ����׶εĲ����Ƕ��Ѿ���ʼ���Ķ�����й�����������׶�����ɵĵ��͹������ܰ������Ҷ����Ƿ�ʵ����һЩ֪ͨ�ӿڣ��Ա��ڳ�ʼ���׶����ʱ���֡�
        /// </summary>
        PostInitialization
    }
}