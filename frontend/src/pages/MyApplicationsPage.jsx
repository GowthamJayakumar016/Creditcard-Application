import { useEffect, useState } from 'react';
import { apiRequest } from '../api';

function MyApplicationsPage() {
  const [items, setItems] = useState([]);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadData() {
      setLoading(true);
      const result = await apiRequest('/applications/my');
      setLoading(false);

      if (!result.ok) {
        setError(result.message);
        return;
      }

      setItems(Array.isArray(result.data) ? result.data : []);
    }

    loadData();
  }, []);

  return (
    <div className="container">
      <div className="card">
        <div className="card-header bg-primary text-white">My Applications</div>
        <div className="card-body">
          {loading && <p>Loading...</p>}
          {error && <div className="alert alert-danger">{error}</div>}
          {!loading && !error && items.length === 0 && <div className="alert alert-info">No applications found.</div>}
          {!loading && !error && items.length > 0 && (
            <div className="table-responsive">
              <table className="table table-bordered table-striped">
                <thead>
                  <tr>
                    <th>Application Number</th>
                    <th>Applied Date</th>
                    <th>Credit Score</th>
                    <th>Credit Limit</th>
                    <th>Status</th>
                  </tr>
                </thead>
                <tbody>
                  {items.map((item) => (
                    <tr key={item.applicationNumber}>
                      <td>{item.applicationNumber}</td>
                      <td>{new Date(item.appliedDate).toLocaleDateString()}</td>
                      <td>{item.creditScore}</td>
                      <td>{item.creditLimit ?? 'Manual Review'}</td>
                      <td>{item.status}</td>
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

export default MyApplicationsPage;
