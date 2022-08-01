# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

[1.14.1] - 2022-08-01
### CHanged
- Changed StringExtensions.GetElementTextOrValue method, so it would always get text value for li elements, otherwise it returns value attribute, which is always index