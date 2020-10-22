export const setFormData = (data) =>  {
	const formData = new FormData();
	for ( const key of Object.keys(data) ) {
		const value = data[key];
		formData.append(key, value);
	}
	return formData;
}
