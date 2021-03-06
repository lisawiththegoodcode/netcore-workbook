﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseProject.Data;
using BaseProject.Models;
using BaseProject.Data.Models;

namespace BaseProject.Controllers
{
    [Route("Issues")]
    public class IssuesController : Controller
    {
        private readonly IssueTrackerContext _context;

        public IssuesController(IssueTrackerContext context)
        {
            _context = context;
        }

        // GET: Issues
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var issueTrackerContext = _context.Issues
                .Include(i => i.Assignee);
            return View(await issueTrackerContext.ToListAsync());
        }

        // GET: Issues/Details/5
        [Route("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.Assignee)
                .Include(i => i.Tasks)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        [Route("Create")]
        public IActionResult Create()
        {
            ViewData["AssigneeId"] = new UserSelectList(_context.Users);
            ViewData["StatusId"] = new StatusSelectList();
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromQuery]bool showCreateAgain, [Bind("Id,Title,Description,Status,AssigneeId")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issue);
                await _context.SaveChangesAsync();
                return showCreateAgain ? RedirectToAction(nameof(Create)) : RedirectToAction(nameof(Index));
            }
            ViewData["AssigneeId"] = issue.AssigneeId.HasValue ? new UserSelectList(_context.Users, issue.AssigneeId.Value) : new UserSelectList(_context.Users);
            ViewData["StatusId"] = new StatusSelectList(issue.Status);
            return View(issue);
        }

        // GET: Issues/Edit/5
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            ViewData["AssigneeId"] = issue.AssigneeId.HasValue ? new UserSelectList(_context.Users, issue.AssigneeId.Value) : new UserSelectList(_context.Users);
            ViewData["StatusId"] = new StatusSelectList(issue.Status);
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,AssigneeId")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssigneeId"] = new SelectList(_context.Users, nameof(Data.Models.User.Id), nameof(Data.Models.User.FullName), issue.AssigneeId);
            ViewData["StatusId"] = new StatusSelectList(issue.Status);
            return View(issue);
        }

        // GET: Issues/Delete/5
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.Assignee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [Route("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
