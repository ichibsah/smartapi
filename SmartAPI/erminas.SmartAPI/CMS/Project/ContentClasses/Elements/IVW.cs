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

namespace erminas.SmartAPI.CMS.Project.ContentClasses.Elements
{
    public interface IIVW
    {
        string Height { get; set; }
        string Src { get; set; }
        string Width { get; set; }
    }

    internal class IVW : ContentClassElement, IIVW
    {
        internal IVW(IContentClass contentClass, XmlElement xmlElement) : base(contentClass, xmlElement)
        {
            CreateAttributes("eltheight", "eltwidth", "eltsrc");
        }

        public override ContentClassCategory Category
        {
            get { return ContentClassCategory.Content; }
        }

        public string Height
        {
            get { return GetAttributeValue<string>("eltheight"); }
            set { SetAttributeValue("eltheight", value); }
        }

        public string Src
        {
            get { return GetAttributeValue<string>("eltsrc"); }
            set { SetAttributeValue("eltsrc", value); }
        }

        public string Width
        {
            get { return GetAttributeValue<string>("eltwidth"); }
            set { SetAttributeValue("eltwidth", value); }
        }
    }
}