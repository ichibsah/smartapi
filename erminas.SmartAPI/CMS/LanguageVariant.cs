﻿/*
 * Smart API - .Net programatical access to RedDot servers
 * Copyright (C) 2012  erminas GbR 
 *
 * This program is free software: you can redistribute it and/or modify it 
 * under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>. 
 */

using System.Xml;
using erminas.SmartAPI.Utils;

namespace erminas.SmartAPI.CMS
{
    public class LanguageVariant : RedDotObject
    {
        private bool _isChecked;
        private string _language;

        public LanguageVariant(Project project, XmlElement xmlElement) : base(xmlElement)
        {
            Project = project;
            LoadXml(xmlElement);
        }

        public string Language
        {
            get { return _language; }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        public Project Project { get; set; }

        public void Select()
        {
            Project.SelectLanguageVariant(this);
        }

        protected override void LoadXml(XmlElement node)
        {
            InitIfPresent(ref _isChecked, "checked", BoolConvert);
            InitIfPresent(ref _language, "language", x => x);

            Name = node.GetAttributeValue("name");
        }
    }
}