#include "Resource.h"

Resource::Resource(int resourceName): resourceName(resourceName) {
}

int Resource::getResourceName() const {
	return resourceName;
}