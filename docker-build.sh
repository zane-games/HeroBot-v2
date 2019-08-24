#!/usr/bin/env bash
docker build . -t registry.gitlab.com/matthisprojects/herobot-csharp/runtime:herobot-v2
docker push registry.gitlab.com/matthisprojects/herobot-csharp/runtime:herobot-v2

