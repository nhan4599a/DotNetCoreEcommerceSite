import axios from "axios";
import AuthHelper from "./AuthHelper";
import SharedConstant from "../shared/CenterRepo";

axios.interceptors.request.use(async (req) => {
	var user = await AuthHelper.getInstance().getManager().getUser();
	req.headers.authorization = `Bearer ${user.access_token}`;
	return req;
});

axios.interceptors.response.use((res) => {
	console.log(res);
	return res;
});

export default class ApiCaller {
	getAllProducts() {
		return axios.get(`${SharedConstant.api_base_uri}/products/all`);
	}

	getAllCategories() {
		return axios.get(`${SharedConstant.api_base_uri}/categories/all`);
	}

	getAllUsers() {
		return axios.get(`${SharedConstant.api_base_uri}/users/all`);
	}
}
