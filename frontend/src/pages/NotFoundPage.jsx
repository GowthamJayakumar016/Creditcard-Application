import { Link } from 'react-router-dom';

function NotFoundPage() {
  return (
    <div className="container">
      <div className="card">
        <div className="card-body">
          <h4>Page not found</h4>
          <Link to="/login" className="btn btn-primary">Go to Login</Link>
        </div>
      </div>
    </div>
  );
}

export default NotFoundPage;
