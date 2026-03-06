import { useState } from 'react';
import { apiRequest } from '../api';

function MyStatusPage() {
  const [applicationNumber, setApplicationNumber] = useState('');
  const [result, setResult] = useState(null);
  const [error, setError] = useState('');

  async function search(e) {
    e.preventDefault();
    setResult(null);
    setError('');

    if (!applicationNumber.trim()) {
      setError('Application number is required.');
      return;
    }

    const response = await apiRequest(`/applications/my/${applicationNumber}`);

    if (!response.ok) {
      setError(response.message);
      return;
    }

    setResult(response.data);
  }

  return (
    <div className="container">
      <div className="card">
        <div className="card-header bg-primary text-white">View Application Status</div>
        <div className="card-body">
          <form onSubmit={search} className="row g-3 mb-3">
            <div className="col-md-8">
              <input
                className="form-control"
                placeholder="Enter application number"
                value={applicationNumber}
                onChange={(e) => setApplicationNumber(e.target.value)}
              />
            </div>
            <div className="col-md-4">
              <button className="btn btn-primary w-100" type="submit">Search</button>
            </div>
          </form>

          {error && <div className="alert alert-danger">{error}</div>}

          {result && (
            <table className="table table-bordered">
              <tbody>
                <tr><th>Application Number</th><td>{result.applicationNumber}</td></tr>
                <tr><th>Credit Score</th><td>{result.creditScore}</td></tr>
                <tr><th>Credit Limit</th><td>{result.creditLimit ?? 'Manual Review'}</td></tr>
                <tr><th>Status</th><td>{result.status}</td></tr>
                <tr><th>Applied Date</th><td>{new Date(result.appliedDate).toLocaleString()}</td></tr>
              </tbody>
            </table>
          )}
        </div>
      </div>
    </div>
  );
}

export default MyStatusPage;
