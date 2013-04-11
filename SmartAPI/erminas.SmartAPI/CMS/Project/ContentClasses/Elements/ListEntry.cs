﻿// Smart API - .Net programmatic access to RedDot servers
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

using System.Xml;
using erminas.SmartAPI.CMS.Project.Filesystem;

namespace erminas.SmartAPI.CMS.Project.ContentClasses.Elements
{
    public interface IListEntry : IContentClassElement
    {
        string EndTagForAutomaticProcessing { get; set; }
        IFolder Folder { get; set; }
        bool IsNotUsedInForm { get; set; }
        bool IsNotVisibleOnPublishedPage { get; set; }
        bool IsUsingEntireTextIfNoMatchingTagsCanBeFound { get; set; }
        string StartTagForAutomaticProcessing { get; set; }
    }

    internal class ListEntry : ContentClassElement, IListEntry
    {
        internal ListEntry(IContentClass contentClass, XmlElement xmlElement) : base(contentClass, xmlElement)
        {
            CreateAttributes("elthideinform", "eltinvisibleinpage", "eltbeginmark", "eltendmark", "eltwholetext",
                             "eltfolderguid");
        }

        public override ContentClassCategory Category
        {
            get { return ContentClassCategory.Content; }
        }

        public string EndTagForAutomaticProcessing
        {
            get { return GetAttributeValue<string>("eltendmark"); }
            set { SetAttributeValue("eltendmark", value); }
        }

        public IFolder Folder
        {
            get { return GetAttributeValue<IFolder>("eltfolderguid"); }
            set { SetAttributeValue("eltfolderguid", value); }
        }

        public bool IsNotUsedInForm
        {
            get { return GetAttributeValue<bool>("elthideinform"); }
            set { SetAttributeValue("elthideinform", value); }
        }

        public bool IsNotVisibleOnPublishedPage
        {
            get { return GetAttributeValue<bool>("eltinvisibleinpage"); }
            set { SetAttributeValue("eltinvisibleinpage", value); }
        }

        public bool IsUsingEntireTextIfNoMatchingTagsCanBeFound
        {
            get { return GetAttributeValue<bool>("eltwholetext"); }
            set { SetAttributeValue("eltwholetext", value); }
        }

        public string StartTagForAutomaticProcessing
        {
            get { return GetAttributeValue<string>("eltbeginmark"); }
            set { SetAttributeValue("eltbeginmark", value); }
        }
    }
}