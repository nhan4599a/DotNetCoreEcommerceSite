import React from "react";

export default class DataTable extends React.Component {
	render() {
		var datasource = this.props.datasource;
		var columnNames = datasource[0] ? Object.keys(datasource[0]) : [];
		return (
			<div className="card">
				<div className="card-body">
					<h5 className="card-title">{this.props.title}</h5>
					<div className="table-responsive">
						<table className="table table-bordered">
							<thead>
								<tr>
									<th scope="col">#</th>
									{columnNames[0] &&
										columnNames.map((columnName) => (
											<th>{columnName}</th>
										))}
								</tr>
							</thead>
							<tbody>
								{datasource[0] &&
									datasource.map((value, index) => (
										<tr>
											<th scope="row">{index + 1}</th>
											{columnNames[0] &&
												columnNames.map(
													(columnName) => (
														<td>
															{value[columnName]}
														</td>
													)
												)}
										</tr>
									))}
							</tbody>
						</table>
					</div>
				</div>
			</div>
		);
	}
}
