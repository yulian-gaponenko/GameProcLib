#ifndef RESOURCE_H
#define RESOURCE_H

class Resource {
private:
	int resourceName;
public:
	Resource(int resourceName);
	int getResourceName() const;
};

#endif