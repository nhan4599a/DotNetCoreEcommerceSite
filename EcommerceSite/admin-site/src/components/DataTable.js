import React from "react";

export default class DataTable extends React.Component {
	render() {
		var datasource = this.props.datasource;
		console.log(datasource[0]);
		var columnNames = Object.keys(datasource[0]);
		return (
			<div className="card">
				<div className="card-body">
					<h5 className="card-title">{this.props.title}</h5>
					<div className="table-responsive">
						<table className="table table-bordered">
							<thead>
								<tr>
									<th scope="col">#</th>
									{columnNames.map((columnName) => (
										<th>{columnName}</th>
									))}
								</tr>
							</thead>
							<tbody>
								{datasource.map((value, index) => (
									<tr>
										<th scope="row">{index + 1}</th>
										{columnNames.map((columnName) => (
											<td>{value[columnName]}</td>
										))}
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
