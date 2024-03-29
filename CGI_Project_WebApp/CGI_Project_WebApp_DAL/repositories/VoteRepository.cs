﻿using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class VoteRepository: IVoteRepository
    {
        public bool TryAddVote(Vote vote)
        {
            Dbi511119Context DBContext = new Dbi511119Context();

            try
            {
                DBContext.Votes.Add(vote);
                DBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool TryAddDateToVote(int voteId, int dateId)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                PreferredDate preferredDate = new PreferredDate();

                preferredDate.DateId = dateId;
                preferredDate.VoteId = voteId;

                DBContext.Add(preferredDate);
                DBContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        

        public bool TryGetVotedSuggestions(out List<Vote> votes)
        {
            using Dbi511119Context dbContext = new();

            try
            {
                votes = dbContext.Votes
                    .AsNoTracking()
                    .Include(v => v.Employee)
                    .Include(v => v.PollSuggestion)
                    .ToList();
                
                return true;
                
            }
            catch (Exception e)
            {
                votes = new List<Vote>();
                return false;
            }
        }
    }
}
