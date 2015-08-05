function validateForm() {
	var valid = false;

	if ($("#chkList input[type=checkbox]:checked").length > 0)
		valid = true;

	return valid;
}