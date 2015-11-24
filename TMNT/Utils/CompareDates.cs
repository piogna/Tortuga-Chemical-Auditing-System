﻿using System;
using System.Linq;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Utils {
    /// <summary>
    /// Util class to check dates in all circumstances
    /// </summary>
    public static class CompareDates {

        /// <summary>
        /// If the balance hasn't been verified on the current date, set it to unverified.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Device SetBalanceToUnverified(Device scale, UnitOfWork _uow) {
            if (scale.DeviceVerifications != null && scale.DeviceVerifications.Count > 0) {
                var test = _uow.DeviceVerificationRepostory.Get()
                    .Where(item => item.Device == scale)
                    .OrderByDescending(item => item.VerifiedOn)
                    .Select(item => item.VerifiedOn)
                    .First();
                if (test.Value.Date < DateTime.Today) {
                    scale.IsVerified = false;
                    scale.Status = "Needs Verification";
                    _uow.BalanceDeviceRepository.Update(scale);
                }
            }
            return scale;
        }
    }
}