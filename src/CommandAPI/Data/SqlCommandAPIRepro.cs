﻿using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Data
{
    public class SqlCommandAPIRepro : ICommandAPIRepo
    {
        private readonly CommandContext _context;
        public SqlCommandAPIRepro(CommandContext context)
        {
            _context = context;
        }
        public void CreateCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.CommandItems.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null) throw new ArgumentNullException();

            _context.CommandItems.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.CommandItems.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.CommandItems.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            _context.CommandItems.Update(cmd);
        }
    }
}
