import { Link } from 'react-router-dom';

function AdminDashboard() {
  return (
    <div className="container">
      <div className="card">
        <div className="card-header bg-primary text-white">Admin Dashboard</div>
        <div className="card-body">
          <Link className="btn btn-primary" to="/admin/applications">View and Manage Applications</Link>
        </div>
      </div>
    </div>
  );
}

export default AdminDashboard;
