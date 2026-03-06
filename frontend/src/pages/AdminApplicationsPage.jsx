import { useEffect, useState } from 'react';
import { apiRequest } from '../api';

function AdminApplicationsPage() {
  const [statusFilter, setStatusFilter] = useState('');
  const [items, setItems] = useState([]);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(true);

  async function loadData(status = '') {
    setLoading(true);
    setError('');

    const query = status ? `?status=${encodeURIComponent(status)}` : '';
    const result = await apiRequest(`/applications/admin${query}`);
    setLoading(false);

    if (!result.ok) {
      setError(result.message);
      return;
    }

    setItems(Array.isArray(result.data) ? result.data : []);
  }

  useEffect(() => {
    loadData('');
  }, []);

  async function updateStatus(applicationNumber, action) {
    const result = await apiRequest(`/applications/admin/${applicationNumber}/${action}`, {
      method: 'PATCH'
    });

    if (!result.ok) {
      setError(result.message);
      return;
    }

    loadData(statusFilter);
  }

  function applyFilter(e) {
    e.preventDefault();
    loadData(statusFilter);
  }

  return (
    <div className="container">
      <div className="card">
        <div className="card-header bg-primary text-white">Admin Applications</div>
        <div className="card-body">
          <form className="row g-2 mb-3" onSubmit={applyFilter}>
            <div className="col-md-4">
              <select className="form-select" value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
                <option value="">All</option>
                <option value="Pending">Pending</option>
                <option value="Approved">Approved</option>
                <option value="Rejected">Rejected</option>
              </select>
            </div>
            <div className="col-md-2">
              <button className="btn btn-primary w-100" type="submit">Filter</button>
            </div>
          </form>

          {loading && <p>Loading...</p>}
          {error && <div className="alert alert-danger">{error}</div>}
          {!loading && !error && items.length === 0 && <div className="alert alert-info">No applications found.</div>}

          {!loading && !error && items.length > 0 && (
            <div className="table-responsive">
              <table className="table table-bordered table-striped">
                <thead>
                  <tr>
                    <th>Application Number</th>
                    <th>User Name</th>
                    <th>PAN</th>
                    <th>Income</th>
                    <th>Credit Score</th>
                    <th>Credit Limit</th>
                    <th>Status</th>
                    <th>Applied Date</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {items.map((item) => (
                    <tr key={item.applicationNumber}>
                      <td>{item.applicationNumber}</td>
                      <td>{item.userName}</td>
                      <td>{item.panNumber}</td>
                      <td>{item.annualIncome}</td>
                      <td>{item.creditScore}</td>
                      <td>{item.creditLimit ?? 'Manual Review'}</td>
                      <td>{item.status}</td>
                      <td>{new Date(item.appliedDate).toLocaleDateString()}</td>
                      <td>
                        <div className="d-flex gap-2">
                          <button
                            className="btn btn-sm btn-primary"
                            onClick={() => updateStatus(item.applicationNumber, 'approve')}
                            disabled={item.status === 'Approved'}
                          >
                            Approve
                          </button>
                          <button
                            className="btn btn-sm btn-outline-primary"
                            onClick={() => updateStatus(item.applicationNumber, 'reject')}
                            disabled={item.status === 'Rejected'}
                          >
                            Reject
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default AdminApplicationsPage;
