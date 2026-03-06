import { Link, useNavigate } from 'react-router-dom';
import { clearAuth, getRole, getUserName, isAuthenticated } from '../auth';

function AppNavbar() {
  const navigate = useNavigate();
  const loggedIn = isAuthenticated();
  const role = getRole();
  const userName = getUserName();

  function logout() {
    clearAuth();
    navigate('/login');
  }

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary mb-4">
      <div className="container">
        <Link className="navbar-brand" to={loggedIn ? (role === 'Admin' ? '/admin' : '/user') : '/login'}>
          Credit Card App
        </Link>

        <div className="collapse navbar-collapse show">
          <ul className="navbar-nav me-auto">
            {loggedIn && role === 'User' && (
              <>
                <li className="nav-item"><Link className="nav-link" to="/user">Dashboard</Link></li>
                <li className="nav-item"><Link className="nav-link" to="/apply">Apply</Link></li>
                <li className="nav-item"><Link className="nav-link" to="/my-applications">My Applications</Link></li>
                <li className="nav-item"><Link className="nav-link" to="/my-status">My Status</Link></li>
              </>
            )}
            {loggedIn && role === 'Admin' && (
              <>
                <li className="nav-item"><Link className="nav-link" to="/admin">Dashboard</Link></li>
                <li className="nav-item"><Link className="nav-link" to="/admin/applications">Applications</Link></li>
              </>
            )}
          </ul>

          <div className="d-flex align-items-center gap-2 text-white">
            {loggedIn && <span>{userName} ({role})</span>}
            {loggedIn ? (
              <button className="btn btn-light btn-sm" onClick={logout}>Logout</button>
            ) : (
              <Link className="btn btn-light btn-sm" to="/login">Login</Link>
            )}
          </div>
        </div>
      </div>
    </nav>
  );
}

export default AppNavbar;
