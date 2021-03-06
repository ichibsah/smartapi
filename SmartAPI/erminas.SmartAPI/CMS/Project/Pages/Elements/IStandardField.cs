﻿// SmartAPI - .Net programmatic access to RedDot servers
//  
// Copyright (C) 2013 erminas GbR
// 
// This program is free software: you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with this program.
// If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Xml;
using erminas.SmartAPI.CMS.Project.ContentClasses;
using erminas.SmartAPI.CMS.Project.ContentClasses.Elements;

namespace erminas.SmartAPI.CMS.Project.Pages.Elements
{
    public interface IStandardField<T> : IValueElement<T>
    {
        T GetValueOrDefault();
        string Description { get; }
        string SampleText { get; }
    }

    internal abstract class StandardField<T> : AbstractValueElement<T>, IStandardField<T>
    {
        private string _description;
        private string _sample;

        protected StandardField(IProject project, XmlElement xmlElement) : base(project, xmlElement)
        {
            LoadXml();
        }

        protected StandardField(IProject project, Guid guid, ILanguageVariant languageVariant)
            : base(project, guid, languageVariant)
        {
        }

        public T GetValueOrDefault()
        {
            if (!EqualityComparer<T>.Default.Equals(Value, default(T)))
            {
                return Value;
            }

            var defaultValue = ((IStandardField) Page.ContentClass.Elements.GetByName(Name)).DefaultValue.ForCurrentLanguage;
            return defaultValue != null ? FromString(defaultValue) : default(T);
        }

        public string Description
        {
            get { return LazyLoad(ref _description); }
        }

        public string SampleText
        {
            get { return LazyLoad(ref _sample); }
        }

        protected override T FromXmlNodeValue(string value)
        {
            return FromString(value);
        }

        protected abstract void LoadWholeStandardField();

        protected override void LoadWholeValueElement()
        {
            LoadXml();
            LoadWholeStandardField();
        }

        private void LoadXml()
        {
            InitIfPresent(ref _sample, "eltrdsample", x => x);
            InitIfPresent(ref _sample, "eltrddescription", x => x);
        }
    }
}