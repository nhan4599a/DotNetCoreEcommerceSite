import axios from "axios";
import AuthHelper from "./AuthHelper";
import SharedConstant from "../shared/CenterRepo";

axios.interceptors.request.use(async (req) => {
	req.url = `${SharedConstant.api_base_uri}/${req.url}`;
	var user = await AuthHelper.getInstance().getManager().getUser();
	if (user) req.headers.authorization = `Bearer ${user.access_token}`;
	return req;
});

axios.interceptors.response.use((res) => {
	return res;
});

export default class ApiCaller {
	getAllProducts() {
		return axios.get("products/all");
	}

	getAllCategories() {
		return axios.get("categories/all");
	}

	getAllUsers() {
		return axios.get("users/all");
	}

	addCategory(categoryName) {
		return axios.post("categories/add", { name: categoryName });
	}

	addProduct(product, categoryId) {
		return axios.post(`products/add?categoryId=${categoryId}`, product, {
			headers: {
				"Content-Type": "multipart/form-data",
			},
		});
	}
}
