﻿//  Copyright 2019-2022 Chris Mohan, Jaben Cargman
//  and GotenbergSharpApiClient Contributors
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using System.Threading.Tasks;

using Gotenberg.Sharp.API.Client.Domain.Builders.Faceted;
using Gotenberg.Sharp.API.Client.Domain.Requests;

using JetBrains.Annotations;

namespace Gotenberg.Sharp.API.Client.Domain.Builders;

public sealed class PdfConversionBuilder : BaseBuilder<PdfConversionRequest, PdfConversionBuilder>
{
    public PdfConversionBuilder()
    {
        this.Request = new PdfConversionRequest();
    }

    [PublicAPI]
    public PdfConversionBuilder SetFormat(PdfFormats format)
    {
        if (format == default) throw new ArgumentNullException(nameof(format));

        this.Request.Format = format;

        return this;
    }

    [PublicAPI]
    public PdfConversionBuilder WithPdfs(Action<AssetBuilder> action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        action(new AssetBuilder(this.Request));

        return this;
    }

    [PublicAPI]
    public PdfConversionBuilder WithPdfsAsync(Func<AssetBuilder, Task> asyncAction)
    {
        if (asyncAction == null) throw new ArgumentNullException(nameof(asyncAction));

        this.AsyncTasks.Add(asyncAction(new AssetBuilder(this.Request)));

        return this;
    }
}