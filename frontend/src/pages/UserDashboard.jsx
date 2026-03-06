import { Link } from 'react-router-dom';

function UserDashboard() {
  return (
    <div className="container">
      <div className="card">
        <div className="card-header bg-primary text-white">User Dashboard</div>
        <div className="card-body">
          <div className="d-flex flex-wrap gap-2">
            <Link className="btn btn-primary" to="/apply">Apply for Credit Card</Link>
            <Link className="btn btn-primary" to="/my-applications">View All Applications</Link>
            <Link className="btn btn-primary" to="/my-status">View Application Status</Link>
          </div>
        </div>
      </div>
    </div>
  );
}

export default UserDashboard;
