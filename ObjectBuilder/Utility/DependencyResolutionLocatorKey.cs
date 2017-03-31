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
    /// ��ʾһ�����ͺ�ID������������λ����Ψһ��ʶ����ǰ��Ҫ������������ͺͱ�ǵ�ǰ�������͵�Ψһ��ʶ
    /// </summary>
    public sealed class DependencyResolutionLocatorKey
    {
        private Type type;
        private string id;

        /// <summary>
        /// ʵ���� <see cref="DependencyResolutionLocatorKey"/> �࣬ ���ͺ�ID��Ϊnull
        /// </summary>
        public DependencyResolutionLocatorKey()
            : this(null, null)
        {
        }

        /// <summary>
        /// ʵ���� <see cref="DependencyResolutionLocatorKey"/> ��
        /// </summary>
        /// <param name="type">��ǰ�������������</param>
        /// <param name="id">��ǰ���������Ψһ���</param>
        public DependencyResolutionLocatorKey(Type type, string id)
        {
            this.type = type;
            this.id = id;
        }

        /// <summary>
        /// ���ص�ǰ��������ı��
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// ���ص�ǰ�������������
        /// </summary>
        public Type Type
        {
            get { return type; }
        }

        /// <summary>
        /// ��дEquals�ȽϺ���
        /// </summary>
        public override bool Equals(object obj)
        {
            DependencyResolutionLocatorKey other = obj as DependencyResolutionLocatorKey;

            if (other == null)
                return false;

            return (Equals(type, other.type) && Equals(id, other.id));
        }

        /// <summary>
        /// ��дGetHashCode����
        /// </summary>
        public override int GetHashCode()
        {
            int hashForType = type == null ? 0 : type.GetHashCode();
            int hashForID = id == null ? 0 : id.GetHashCode();
            return hashForType ^ hashForID;
        }
    }
}