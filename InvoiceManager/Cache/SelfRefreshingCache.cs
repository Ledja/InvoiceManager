using Microsoft.Extensions.Logging;
using System;
using System.Timers;

namespace SelfRefreshingCache.Cache
{
    public class SelfRefreshingCache<TResult>
    {
        static readonly object _object = new object();
        private bool _isDataValid = true;
        private static TResult _cachedData;
        private ILogger<TResult> _logger;
        private Func<TResult> _createdCachedItem;
        private Timer refreshPeriodTimer;
        private Timer validityOfCachedDataTimer;

        public SelfRefreshingCache(ILogger<TResult> logger, TimeSpan refreshPeriod, TimeSpan validityOfCachedData, Func<TResult> createdCachedItem)
        {
            _logger = logger;
            _createdCachedItem = createdCachedItem;

            refreshPeriodTimer = new Timer();
            refreshPeriodTimer.Elapsed += new ElapsedEventHandler(OnRefreshPeriodEvent);
            refreshPeriodTimer.Interval = refreshPeriod.TotalMilliseconds;

            validityOfCachedDataTimer = new Timer();
            validityOfCachedDataTimer.Elapsed += new ElapsedEventHandler(OnValidityPeriodEvent);
            validityOfCachedDataTimer.Interval = validityOfCachedData.TotalMilliseconds;

        }

        private void OnValidityPeriodEvent(object sender, ElapsedEventArgs e)
        {
            _isDataValid = false;
        }

        private void OnRefreshPeriodEvent(object sender, ElapsedEventArgs e)
        {

            SetCachedData();
        }

        public TResult GetOrCreate()
        {
            lock (_object)
            {
                if (!_isDataValid) return default;
                if (_cachedData != null) return _cachedData;

                SetCachedData();

                refreshPeriodTimer.Start();
                validityOfCachedDataTimer.Start();

                return _cachedData;
            }

        }

        private void SetCachedData()
        {
            try
            {
                lock (_object)
                {
                    _cachedData = _createdCachedItem.Invoke();
                    validityOfCachedDataTimer.Stop();
                    validityOfCachedDataTimer.Start();
                    _isDataValid = true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to set cached data", e);
            }
        }
    }
}
