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

using System.Collections.Generic;
using erminas.SmartAPI.CMS.Project.Keywords;
using erminas.SmartAPI.Utils.CachedCollections;

namespace erminas.SmartAPI.CMS.Project.Pages
{
    public interface IAssignedKeywords : IRDList<IKeyword>
    {
        void Add(IKeyword keyword);
        void AddRange(IEnumerable<IKeyword> keywords);
        void Clear();
        void Remove(IKeyword keyword);
        void Set(IEnumerable<IKeyword> keywords);
    }
}