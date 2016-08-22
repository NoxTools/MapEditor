using System;
using System.IO;
//using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

public abstract class Bag// : Observable
{
	private enum BagType : uint
	{
		GABA = 0x41424147,//"GABA" -- not a valid type!!, must be GABA2 or GABA4
		GABA2 = 0x02,//Nox audio
		GABA4 = 0x04,//Emperor audio
		VIDEO = 0xFAEDBCEB//Nox video
	}
		
	protected FileStream bag;
	protected FileStream idx;
	protected string bagPath;

	//caller will use this to determine the right class and then
	//construct it himself
	public static Type GetBagType(string path)
	{
		BagType type;
		Type bagClass = null;
		FileStream idx;

		string idxPath = path.Replace(".bag",".idx");
		if (File.Exists(idxPath))
			idx = File.Open(idxPath, FileMode.OpenOrCreate);
		else
			idx = File.Open(path, FileMode.OpenOrCreate);

		BinaryReader rdr = new BinaryReader(idx);

		type = (BagType) rdr.ReadInt32();
		if (type == BagType.GABA)
			type = (BagType) rdr.ReadInt32();

		rdr.Close();

		//only support GABA2 and VIDEO for now
		switch (type)
		{
				//HACK: these can break if namespace or class names change, FIXME
			case BagType.VIDEO:
				bagClass = Type.GetType("NoxBagTool.VideoBag");
				break;
			case BagType.GABA2:
				bagClass = Type.GetType("NoxBagTool.Gaba2Bag");
				break;
		}

		return bagClass;
	}

	public Bag(string path)// : base()
	{
		bagPath = path;
	}

	protected virtual bool Read()
	{
        if (!File.Exists(bagPath))
        {
            throw new FileNotFoundException("Specified .bag was not found.", bagPath);
        }
        try
        {
            bag = File.Open(bagPath, FileMode.OpenOrCreate, FileAccess.Read);
        }
        catch (Exception)
        {
            return false;
        }
		//TODO:enforce .bag extension??

		//open .idx if it is avalaible
		//string idxPath = Regex.Replace(bag.Name, "\\.bag$", ".idx");
		string idxPath = bag.Name.Replace(".bag",".idx");
		if (File.Exists(idxPath))
			idx = File.Open(idxPath, FileMode.OpenOrCreate, FileAccess.Read);
		else
			idx = bag;//.bag will serve as .idx if not found
		//TODO: separate IDX into a separate class and manage IDX and BAG separately?
        return true;
	}

	public abstract void ExtractAll(string path);

	//preconditions: type has been initialized
	protected void Write()
	{
	}
}