using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TMNT.Models.Enums;

namespace TMNT.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        private DeviceRepository _deviceRepository;
        private InventoryItemRepository _inventoryItemRepository;
        private BalanceDeviceRepository _balanceDeviceRepository;
        private CertificateOfAnalysisRepository _certificateOfAnalysisRepository;
        private DepartmentRepository _departmentRepository;
        private DeviceVerificationRepostory _deviceVerificationRepository;
        private IntermediateStandardRepository _intermediateStandardRepository;
        private LocationRepository _locationRepository;
        private LowStockRepository _lowStockRepository;
        private CofARepository _cofARepository;
        private MSDSRepository _mSDSRepository;
        private PreparedReagentRepository _preparedReagentRepository;
        private PreparedStandardRepository _preparedStandardRepository;
        private PrepListItemRepository _prepListItemRepository;
        private PrepListRepository _prepListRepository;
        private StockReagentRepository _stockReagentRepository;
        private StockStandardRepository _stockStandardRepository;
        private UnitRepository _unitRepository;
        private VolumetricDeviceRepository _volumetricDeviceRepository;
        private WorkingStandardRepository _workingStandardRepository;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public UnitOfWork() : this(new ApplicationDbContext())
        {
            
        }

        public DeviceRepository DeviceRepository
        {
            get
            {
                if (_deviceRepository == null)
                {
                    _deviceRepository = new DeviceRepository(_db);
                }
                return _deviceRepository;
            }
        }

        
        public InventoryItemRepository InventoryItemRepository
        {
            get
            {
                if (_inventoryItemRepository == null)
                {
                    _inventoryItemRepository = new InventoryItemRepository(_db);
                }
                return _inventoryItemRepository;
            }
        }

        public BalanceDeviceRepository BalanceDeviceRepository
        {
            get
            {
                if (_balanceDeviceRepository == null)
                {
                    _balanceDeviceRepository = new BalanceDeviceRepository(_db);
                }
                return _balanceDeviceRepository;
            }
        }
        public CertificateOfAnalysisRepository CertificateOfAnalysisRepository
        {
            get
            {
                if (_certificateOfAnalysisRepository == null)
                {
                    _certificateOfAnalysisRepository = new CertificateOfAnalysisRepository(_db);
                }
                return _certificateOfAnalysisRepository;
            }
        }
        public DepartmentRepository DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository(_db);
                }
                return _departmentRepository;
            }
        }
        public DeviceVerificationRepostory DeviceVerificationRepostory
        {
            get
            {
                if (_deviceVerificationRepository == null)
                {
                    _deviceVerificationRepository = new DeviceVerificationRepostory(_db);
                }
                return _deviceVerificationRepository;
            }
        }
        public IntermediateStandardRepository IntermediateStandardRepository
        {
            get
            {
                if (_intermediateStandardRepository == null)
                {
                    _intermediateStandardRepository = new IntermediateStandardRepository(_db);
                }
                return _intermediateStandardRepository;
            }
        }
        public LocationRepository LocationRepository
        {
            get
            {
                if (_locationRepository == null)
                {
                    _locationRepository = new LocationRepository(_db);
                }
                return _locationRepository;
            }
        }
        public LowStockRepository LowStockRepository
        {
            get
            {
                if (_lowStockRepository == null)
                {
                    _lowStockRepository = new LowStockRepository(_db);
                }
                return _lowStockRepository;
            }
        }
        public CofARepository CofARepository
        {
            get
            {
                if (_cofARepository == null)
                {
                    _cofARepository = new CofARepository(_db);
                }
                return _cofARepository;
            }
        }
        public MSDSRepository MSDSRepository
        {
            get
            {
                if (_mSDSRepository == null)
                {
                    _mSDSRepository = new MSDSRepository(_db);
                }
                return _mSDSRepository;
            }
        }
        public PreparedReagentRepository PreparedReagentRepository
        {
            get
            {
                if (_preparedReagentRepository == null)
                {
                    _preparedReagentRepository = new PreparedReagentRepository(_db);
                }
                return _preparedReagentRepository;
            }
        }
        public PreparedStandardRepository PreparedStandardRepository
        {
            get
            {
                if (_preparedStandardRepository == null)
                {
                    _preparedStandardRepository = new PreparedStandardRepository(_db);
                }
                return _preparedStandardRepository;
            }
        }
        public PrepListItemRepository PrepListItemRepository
        {
            get
            {
                if (_prepListItemRepository == null)
                {
                    _prepListItemRepository = new PrepListItemRepository(_db);
                }
                return _prepListItemRepository;
            }
        }
        public PrepListRepository PrepListRepository
        {
            get
            {
                if (_prepListRepository == null)
                {
                    _prepListRepository = new PrepListRepository(_db);
                }
                return _prepListRepository;
            }
        }
        public StockReagentRepository StockReagentRepository
        {
            get
            {
                if (_stockReagentRepository == null)
                {
                    _stockReagentRepository = new StockReagentRepository(_db);
                }
                return _stockReagentRepository;
            }
        }
        public StockStandardRepository StockStandardRepository
        {
            get
            {
                if (_stockStandardRepository == null)
                {
                    _stockStandardRepository = new StockStandardRepository(_db);
                }
                return _stockStandardRepository;
            }
        }
        public UnitRepository UnitRepository
        {
            get
            {
                if (_unitRepository == null)
                {
                    _unitRepository = new UnitRepository(_db);
                }
                return _unitRepository;
            }
        }
        public VolumetricDeviceRepository VolumetricDeviceRepository
        {
            get
            {
                if (_volumetricDeviceRepository == null)
                {
                    _volumetricDeviceRepository = new VolumetricDeviceRepository(_db);
                }
                return _volumetricDeviceRepository;
            }
        }
        public WorkingStandardRepository workingStandardRepository
        {
            get
            {
                if (_workingStandardRepository == null)
                {
                    _workingStandardRepository = new WorkingStandardRepository(_db);
                }
                return _workingStandardRepository;
            }
        }

        public CheckModelState Commit()
        {
            try
            {
                _db.SaveChanges();
                return CheckModelState.Valid;
            }
            catch (DbUpdateConcurrencyException)
            {
                return CheckModelState.ConcurrencyError;
            }
            catch (DbUpdateException)
            {
                return CheckModelState.UpdateError;
            }
            catch (DbEntityValidationException)
            {
                return CheckModelState.Invalid;
            }
            catch(ObjectDisposedException) 
            {
                return CheckModelState.Disposed;         
            }
            catch (DataException)
            {
                return CheckModelState.DataError;
            }
            catch (Exception)
            {
                return CheckModelState.Error;
            }
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}