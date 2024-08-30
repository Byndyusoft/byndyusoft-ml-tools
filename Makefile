PYTHON_BASE_IMAGE="mcr.microsoft.com/dotnet/sdk:6.0"

define docker-run
	docker run \
		--rm -t \
		-v $(CURDIR):/app \
		-w /app \
		$(PYTHON_BASE_IMAGE) \
		sh -x -c "$(strip $(1))"
endef

################################################################################
.PHONY: build-app
build-app:
	$(call docker-run, dotnet build && dotnet pack -o /app/nuget --configuration Debug)
